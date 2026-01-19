export interface Contact {
  id: number;
  name: string;
  mobilePhone: string;
  jobTitle: string;
  birthDate: string;
}

export interface CreateContactRequest {
  name: string;
  mobilePhone: string;
  jobTitle: string;
  birthDate: string;
}

export interface UpdateContactRequest extends CreateContactRequest {
  id: number;
}
