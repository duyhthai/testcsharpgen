export class Employee {
  protected name: string;
  protected salary: number;

  constructor(name: string, salary: number) {
    this.name = name;
    this.salary = salary;
  }

  work(): void {
    console.log(`${this.name} is working.`);
  }

  getDetails(): string {
    return `Employee: ${this.name}, Salary: $${this.salary}`;
  }
}
