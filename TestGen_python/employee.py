class Employee:
    def __init__(self, name, salary):
        self.name = name
        self.salary = salary

    def work(self):
        print(f"{self.name} is working.")

    def get_details(self):
        return f"Employee: {self.name}, Salary: ${self.salary}"
