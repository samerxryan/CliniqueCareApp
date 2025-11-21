export interface Employee {
  id: number;
  email: string;
  adresse: string;
  telephone: string;
  nom: string;
  prenom: string;
  cin: string;
  dateNaissance: string;
  poste: string;
  salaire: string;
}

export interface Contract {
  id: number;
  type: string;
  employe: string;
  dateDebut: string;
  dateFin: string;
}

export interface Presence {
  id: number;
  employe: string;
  date: string;
  status: string;
}

export interface Leave {
  id: number;
  employe: string;
  type: string;
  debut: string;
  fin: string;
}

export interface Payroll {
  id: number;
  employe: string;
  mois: string;
  montant: string;
}

export interface Training {
  id: number;
  titre: string;
  employe: string;
  date: string;
}

export interface DatabaseSchema {
  employes: Employee[];
  contrats: Contract[];
  presences: Presence[];
  conges: Leave[];
  paie: Payroll[];
  formations: Training[];
}
