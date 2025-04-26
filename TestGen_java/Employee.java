public class Employee {
    private String name;
    private double salary;

    public Employee(String name, double salary) {
        this.name = name;
        this.salary = salary;
    }

    public void work() {
        System.out.println(name + " is working on something.");
    }

    public String getDetails() {
        return "Employee: " + name + ", Salary: $" + salary;
    }

    // Getters and Setters (optional)
    public String getName() {
        return name;
    }

    public double getSalary() {
        return salary;
    }
}
