from employee import Employee

class Manager(Employee):
    def __init__(self, name, salary, team_size):
        super().__init__(name, salary)
        self.team_size = team_size

    def work(self):
        print(f"{self.name} is managing a team of {self.team_size} employees.")

    def get_details(self):
        return f"{super().get_details()}, Manages: {self.team_size} people"
