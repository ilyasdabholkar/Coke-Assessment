import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private employees = new BehaviorSubject<any[]>([
    { id: 1, name: 'Zebra' },
    { id: 3, name: 'Alice' },
    { id: 2, name: 'Bob' },
    { id: 4, name: 'John' },
    { id: 5, name: 'Freddy' },
  ]);

  employees$ = this.employees.asObservable();


  addEmployee(newEmp: any) {
    setTimeout(() => {
      let current = this.employees.value;
      current.push(newEmp);
      this.employees.next([...current]);
    }, 1000); 
  }

  deleteEmployee(id: number) {
    let current = this.employees.value;
    let filtered = current.filter(e => e.id !== id);
    this.employees.value.splice(0); 
    this.employees.next(this.employees.value); 
  }

}
