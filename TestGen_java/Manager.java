public class Manager extends Employee {
  private int teamSize;

  public Manager(String name, double salary, int teamSize) {
      super(name, salary); // Call the constructor of Employee
      this.teamSize = teamSize;
  }

  @Override
  public void work() {
      System.out.println(getName() + " is managing a team of " + teamSize + " employees.");
  }

  @Override
  public String getDetails() {
      return super.getDetails() + ", Manages: " + teamSize + " people";
  }
}
