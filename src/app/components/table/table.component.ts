import { Component, Input, Output, EventEmitter } from '@angular/core';
import { DataService } from '../../services/data.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent {
  @Input() currentView: string = '';
  @Input() items: any[] = [];
  @Input() columns: string[] = [];
  @Output() openForm = new EventEmitter<number | null>();

  constructor(private dataService: DataService) {}

  capitalize(str: string): string {
    if (!str) return '';
    return str.charAt(0).toUpperCase() + str.slice(1);
  }

  escapeHtml(text: any): string {
    if (text === null || text === undefined) return '';
    return String(text)
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;')
      .replace(/'/g, '&#039;');
  }

  onEdit(id: number): void {
    this.openForm.emit(id);
  }

  onDelete(entityName: string, id: number): void {
    Swal.fire({
      title: 'Êtes-vous sûr ?',
      text: `Cette action supprimera définitivement ${entityName}.`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Oui, supprimer',
      cancelButtonText: 'Annuler'
    }).then(result => {
      if (result.isConfirmed) {
        this.dataService.deleteItem(this.currentView as any, id);
        this.showDeleteSuccess(entityName);
      }
    });
  }

  private showDeleteSuccess(entity: string): void {
    Swal.fire({
      toast: true,
      position: 'top-end',
      icon: 'success',
      title: `${entity} supprimé`,
      showConfirmButton: false,
      timer: 1500
    });
  }
}
