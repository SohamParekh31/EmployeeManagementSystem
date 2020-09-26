import { Department } from './Department';

export interface Employee {
  id: number;
  name: string;
  surname: string;
  address: string;
  qualification: string;
  contact_Number: number;
  email?:string;
  departmentId: number;
  department?: Department;
}
