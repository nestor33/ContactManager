import Contact from './contracts/Contact';

export const getContacts = async (): Promise<Contact[]> => {
  return (await fetch('/Contacts')).json();
};
