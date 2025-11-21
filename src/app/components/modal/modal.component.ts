import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { DataService } from '../../services/data.service';
import { DatabaseSchema } from '../../models/data.models';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {
  @Input() showModal = false;
  @Input() currentView: string = '';
  @Input() editId: number | null = null;
  @Output() closeModal = new EventEmitter<void>();
  @Output() itemSaved = new EventEmitter<void>();

  formData: Record<string, string> = {};
  db: DatabaseSchema | null = null;

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    this.dataService.db$.subscribe((db: DatabaseSchema) => {
      this.db = db;
    });
  }

  ngOnChanges(): void {
    if (this.showModal && this.editId) {
      this.loadItemData();
    } else if (this.showModal) {
      this.formData = {};
    }
  }

  private loadItemData(): void {
    if (!this.db || !this.editId) return;
    const section = this.currentView as keyof DatabaseSchema;
    const items = this.db[section] as any[];
    const item = items.find(x => x.id === this.editId);
    if (item) {
      this.formData = { ...item };
    }
  }

  onCloseModal(): void {
    this.closeModal.emit();
  }

  onSave(): void {
    // Validation
    if (this.currentView === 'employes' && !(this.formData['nom'] || this.formData['prenom'])) {
      Swal.fire('Erreur', 'Le nom ou le prénom est requis pour un employé.', 'error');
      return;
    }

    // Ensure currency format for salary fields
    if (this.formData['salaire'] && !/tnd/i.test(this.formData['salaire'])) {
      this.formData['salaire'] = this.formData['salaire'].trim() + ' TND';
    }
    if (this.formData['montant'] && !/tnd/i.test(this.formData['montant'])) {
      this.formData['montant'] = this.formData['montant'].trim() + ' TND';
    }

    const section = this.currentView as keyof DatabaseSchema;

    if (this.editId) {
      this.dataService.updateItem(section, this.editId, this.formData);
      this.showUpdateSuccess(this.capitalize(this.currentView));
    } else {
      const newItem = {
        id: Date.now(),
        ...this.formData
      };
      this.dataService.addItem(section, newItem);
      this.showAddSuccess(this.capitalize(this.currentView));
    }

    this.closeModal.emit();
    this.itemSaved.emit();
  }

  capitalize(str: string): string {
    if (!str) return '';
    return str.charAt(0).toUpperCase() + str.slice(1);
  }

  private showAddSuccess(entity: string): void {
    Swal.fire({
      toast: true,
      position: 'top-end',
      icon: 'success',
      title: `${entity} ajouté`,
      showConfirmButton: false,
      timer: 1500
    });
  }

  private showUpdateSuccess(entity: string): void {
    Swal.fire({
      toast: true,
      position: 'top-end',
      icon: 'info',
      title: `${entity} mis à jour`,
      showConfirmButton: false,
      timer: 1500
    });
  }
}
