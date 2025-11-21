import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, finalize } from 'rxjs/operators';
import { DatabaseSchema, Employee, Contract, Presence, Leave, Payroll, Training } from '../models/data.models';
import { BackendApiService } from './backend-api.service';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private readonly STORAGE_KEY = 'cliniquecare_db_v1';
  
  private defaultDB: DatabaseSchema = {
    employes: [
      {
        id: 1,
        email: 'sami@clinique.tn',
        adresse: 'Ariana',
        telephone: '20202020',
        nom: 'Sami',
        prenom: 'Ben Ali',
        cin: '12345678',
        dateNaissance: '1985-06-15',
        poste: 'Médecin Généraliste',
        salaire: '3500 TND'
      },
      {
        id: 2,
        email: 'amina@clinique.tn',
        adresse: 'Tunis',
        telephone: '29123456',
        nom: 'Amina',
        prenom: 'Trabelsi',
        cin: '87654321',
        dateNaissance: '1990-03-02',
        poste: 'Infirmière',
        salaire: '1200 TND'
      }
    ],
    contrats: [
      {
        id: 1,
        type: 'CDI',
        employe: 'Sami Ben Ali',
        dateDebut: '2025-01-01',
        dateFin: '2026-01-01'
      },
      {
        id: 2,
        type: 'CDD',
        employe: 'Amina Trabelsi',
        dateDebut: '2025-06-01',
        dateFin: '2025-12-01'
      }
    ],
    presences: [
      {
        id: 1,
        employe: 'Sami Ben Ali',
        date: '2025-10-01',
        status: 'Présent'
      },
      {
        id: 2,
        employe: 'Amina Trabelsi',
        date: '2025-10-01',
        status: 'Absent'
      }
    ],
    conges: [
      {
        id: 1,
        employe: 'Sami Ben Ali',
        type: 'Annuel',
        debut: '2025-08-01',
        fin: '2025-08-10'
      },
      {
        id: 2,
        employe: 'Amina Trabelsi',
        type: 'Maladie',
        debut: '2025-09-05',
        fin: '2025-09-08'
      }
    ],
    paie: [
      {
        id: 1,
        employe: 'Sami Ben Ali',
        mois: 'Septembre 2025',
        montant: '3500 TND'
      },
      {
        id: 2,
        employe: 'Amina Trabelsi',
        mois: 'Septembre 2025',
        montant: '1200 TND'
      }
    ],
    formations: [
      {
        id: 1,
        titre: 'Hygiène Hospitalière',
        employe: 'Sami Ben Ali',
        date: '2025-11-15'
      }
    ]
  };

  private db: DatabaseSchema;
  private dbSubject = new BehaviorSubject<DatabaseSchema>(this.defaultDB);
  public db$: Observable<DatabaseSchema> = this.dbSubject.asObservable();
  private loading = false;

  constructor(private apiService: BackendApiService) {
    this.db = this.defaultDB;
    this.loadDataFromBackend();
  }

  /**
   * Load all data from the backend API on initialization.
   */
  private loadDataFromBackend(): void {
    if (this.loading) return;
    this.loading = true;

    // Fetch all data from the backend in parallel and update the local cache.
    Promise.all([
      this.apiService.getEmployees().toPromise(),
      this.apiService.getContracts().toPromise(),
      this.apiService.getPresences().toPromise(),
      this.apiService.getLeaves().toPromise(),
      this.apiService.getPayroll().toPromise(),
      this.apiService.getTrainings().toPromise()
    ])
      .then(([employees, contracts, presences, leaves, payroll, trainings]) => {
        this.db.employes = employees || [];
        this.db.contrats = contracts || [];
        this.db.presences = presences || [];
        this.db.conges = leaves || [];
        this.db.paie = payroll || [];
        this.db.formations = trainings || [];
        this.dbSubject.next({ ...this.db });
      })
      .catch(err => {
        console.error('Error loading data from backend:', err);
        // Fallback to default data if backend fails
        this.dbSubject.next({ ...this.db });
      })
      .finally(() => {
        this.loading = false;
      });
  }

  /**
   * Reload all data from the backend (useful after create/update/delete operations).
   */
  reloadFromBackend(): void {
    this.loadDataFromBackend();
  }

  private loadDB(): DatabaseSchema | null {
    try {
      const raw = localStorage.getItem(this.STORAGE_KEY);
      if (raw) {
        return JSON.parse(raw);
      }
    } catch (e) {
      console.error('Erreur parse localStorage:', e);
      localStorage.removeItem(this.STORAGE_KEY);
    }
    return null;
  }

  private saveDB(): void {
    try {
      localStorage.setItem(this.STORAGE_KEY, JSON.stringify(this.db));
      this.dbSubject.next({ ...this.db });
    } catch (e) {
      console.error('Erreur de sauvegarde localStorage:', e);
    }
  }

  // Generic methods
  getItems<T>(section: keyof DatabaseSchema): T[] {
    return (this.db[section] as T[]) || [];
  }

  addItem(section: keyof DatabaseSchema, item: any): void {
    if (!this.db[section]) {
      this.db[section] = [];
    }
    (this.db[section] as any[]).push(item);
    this.saveDB();
  }

  updateItem(section: keyof DatabaseSchema, id: number, item: any): void {
    const items = this.db[section] as any[];
    const index = items.findIndex(x => x.id === id);
    if (index > -1) {
      items[index] = item;
      this.saveDB();
    }
  }

  deleteItem(section: keyof DatabaseSchema, id: number): void {
    this.db[section] = (this.db[section] as any[]).filter(e => e.id !== id);
    this.saveDB();
  }

  // Specific getters
  getEmployees(): Employee[] {
    return this.getItems<Employee>('employes');
  }

  getContracts(): Contract[] {
    return this.getItems<Contract>('contrats');
  }

  getPresences(): Presence[] {
    return this.getItems<Presence>('presences');
  }

  getLeaves(): Leave[] {
    return this.getItems<Leave>('conges');
  }

  getPayroll(): Payroll[] {
    return this.getItems<Payroll>('paie');
  }

  getTrainings(): Training[] {
    return this.getItems<Training>('formations');
  }
}
