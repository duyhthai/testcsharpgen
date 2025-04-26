import org.junit.jupiter.api.Test;
import static org.mockito.Mockito.*;

public class EmployeeTest {

    @Test
    public void testWorkWithValidName() {
        // Arrange
        Employee employee = new Employee();
        employee.name = "John";

        // Act
        employee.work();

        // Assert
        verifyNoMoreInteractions(employee);
        System.out.println("John is working on something.");
    }

    @Test
    public void testWorkWithEmptyName() {
        // Arrange
        Employee employee = new Employee();
        employee.name = "";

        // Act
        employee.work();

        // Assert
        verifyNoMoreInteractions(employee);
        System.out.println(" is working on something.");
    }

    @Test
    public void testWorkWithNullName() {
        // Arrange
        Employee employee = new Employee();
        employee.name = null;

        // Act
        employee.work();

        // Assert
        verifyNoMoreInteractions(employee);
        System.out.println("null is working on something.");
    }
}