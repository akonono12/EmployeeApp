import { Component, OnInit } from '@angular/core';
import { Employee } from '../../models/employee.model';
import { EmployeeService } from 'src/app/employee.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  employees: Employee[] = [];
  selected: Employee | null = null;

  constructor(private svc: EmployeeService) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.svc.getAll().subscribe(data => this.employees = data);
  }

  onSave(emp: Employee) {
    if (emp.id) {
      this.svc.update(emp.id, emp).subscribe(() => this.load());
    } else {
      this.svc.create({ firstname: emp.firstname, lastname: emp.lastname })
        .subscribe(() => this.load());
    }
    this.selected = null;
  }

  onEdit(emp: Employee) {
    this.selected = { ...emp };
  }

  onDelete(id: string) {
    this.svc.delete(id).subscribe(() => this.load());
  }
}
