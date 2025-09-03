import { Component, OnInit } from '@angular/core';
import { EmployeeService } from './employee.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
   employees: any[] = [];
  newName: string = '';
  searchTerm: string = '';
  sortBy: string = 'id';

  constructor(private empService: EmployeeService) {}

  ngOnInit() {
    this.empService.employees$.subscribe(list => this.employees = list);
  }

  add() {
    let id = this.employees.length + 1;
    this.empService.addEmployee({ id, name: this.newName });
    this.newName = '';
  }

  delete(id: number) {
    this.empService.deleteEmployee(id);
  }

  filteredEmployees() {
  let filtered = this.employees.filter(e =>
    e.name.includes(this.searchTerm)  
  );

  if (this.sortBy === 'name') {
    filtered.sort((a, b) => (a.name > b.name ? 1 : -1));
  } else {
    filtered.sort((a, b) => b.id - a.id);
  }

  return filtered;
}
}