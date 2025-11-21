import { Component, OnInit } from '@angular/core';
import { DataService } from './services/data.service';
import { DatabaseSchema } from './models/data.models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  currentView: string = 'employes';
  showModal = false;
  editId: number | null = null;
  db: DatabaseSchema | null = null;

  columns: Record<string, string[]> = {
    employes: ['id', 'email', 'adresse', 'telephone', 'nom', 'prenom', 'cin', 'dateNaissance', 'poste', 'salaire'],
    contrats: ['id', 'type', 'employe', 'dateDebut', 'dateFin'],
    presences: ['id', 'employe', 'date', 'status'],
    conges: ['id', 'employe', 'type', 'debut', 'fin'],
    paie: ['id', 'employe', 'mois', 'montant'],
    formations: ['id', 'titre', 'employe', 'date']
  };

  items: any[] = [];

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    this.dataService.db$.subscribe(db => {
      this.db = db;
      this.updateItems();
    });
  }

  showView(view: string): void {
    this.currentView = view;
    this.updateItems();
  }

  updateItems(): void {
    if (!this.db) return;
    const section = this.currentView as keyof DatabaseSchema;
    this.items = this.db[section] || [];
  }

  openForm(id: number | null = null): void {
    this.editId = id;
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
    this.editId = null;
  }

  capitalize(str: string): string {
    if (!str) return '';
    return str.charAt(0).toUpperCase() + str.slice(1);
  }
}
