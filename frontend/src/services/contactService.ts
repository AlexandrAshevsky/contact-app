import type { Contact, CreateContactRequest, UpdateContactRequest } from '../types/contact';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5233/api';

export const contactService = {
  async getAll(): Promise<Contact[]> {
    const response = await fetch(`${API_BASE_URL}/Contact/GetAll`);
    if (!response.ok) {
      throw new Error('Failed to fetch contacts');
    }
    return response.json();
  },

  async getById(id: number): Promise<Contact> {
    const response = await fetch(`${API_BASE_URL}/Contact?id=${id}`);
    if (!response.ok) {
      throw new Error('Failed to fetch contact');
    }
    return response.json();
  },

  async create(contact: CreateContactRequest): Promise<Contact> {
    const response = await fetch(`${API_BASE_URL}/Contact`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(contact),
    });
    if (!response.ok) {
      throw new Error('Failed to create contact');
    }
    return response.json();
  },

  async update(contact: UpdateContactRequest): Promise<Contact> {
    const response = await fetch(`${API_BASE_URL}/Contact`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(contact),
    });
    if (!response.ok) {
      throw new Error('Failed to update contact');
    }
    return response.json();
  },

  async delete(id: number): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Contact?id=${id}`, {
      method: 'DELETE',
    });
    if (!response.ok) {
      throw new Error('Failed to delete contact');
    }
  },
};
