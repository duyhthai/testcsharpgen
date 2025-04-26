import org.junit.jupiter.api.Test;
import static org.mockito.Mockito.*;

public class EmployeeTest {

    @Test
    public void testWorkWithBoundaryLengthName() {
        Employee employee = new Employee();
        employee.name = "a".repeat(255); // Assuming name has a maximum length of 255 characters
        employee.work();
        verifyNoMoreInteractions(employee);
    }

    @Test
    public void testWorkWithEmptyBoundaryLengthName() {
        Employee employee = new Employee();
        employee.name = "".repeat(255); // Assuming name has a maximum length of 255 characters
        employee.work();
        verifyNoMoreInteractions(employee);
    }

    @Test
    public void testWorkWithLongerThanBoundaryLengthName() {
        Employee employee = new Employee();
        employee.name = "a".repeat(256); // Assuming name has a maximum length of 255 characters
        employee.work();
        verifyNoMoreInteractions(employee);
    }
}