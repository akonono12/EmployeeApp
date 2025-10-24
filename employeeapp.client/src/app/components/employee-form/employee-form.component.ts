import { Component, EventEmitter, Input, Output, OnChanges, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Employee } from '../../models/employee.model';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.css']
})
export class EmployeeFormComponent implements OnChanges {
  @Input() employee: Employee | null = null;
  @Output() save = new EventEmitter<Employee>();

  form = new FormGroup({
    firstname: new FormControl('', [Validators.required, Validators.minLength(2)]),
    lastname: new FormControl('', [Validators.required, Validators.minLength(2)])
  });

  ngOnChanges(changes: SimpleChanges) {
    if (changes['employee'] && this.employee) {
      this.form.patchValue(this.employee);
    }
  }

  onSubmit() {
    if (this.form.valid) {
      const value = this.form.value as Employee;
      this.save.emit({ ...this.employee, ...value });
      this.form.reset();
    }
  }

  reset() {
    this.form.reset();
  }
}
