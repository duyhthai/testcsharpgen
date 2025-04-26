import { Employee } from './Employee';

export class Manager extends Employee {
  private teamSize: number;

  constructor(name: string, salary: number, teamSize: number) {
    super(name, salary);
    this.teamSize = teamSize;
  }

  work(): void {
    console.log(`${this.name} is managing a team of ${this.teamSize} employees.`);
  }

  getDetails(): string {
    return `${super.getDetails()}, Manages: ${this.teamSize} people`;
  }
}
