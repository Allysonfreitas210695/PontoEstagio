export type UniversityDTO = {
  id: string;
  name: string;
  acronym: string;
  cnpj: string;
  email: string;
  phone: string;
  isActive: string;
  address: AddressDTO;
};

export type AddressDTO = {
  street: string;
  number: string;
  district: string;
  city: string;
  state: string;
  zipCode: string;
  complement: string;
};
