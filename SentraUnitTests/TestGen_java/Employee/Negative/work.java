import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class EmployeeTest {

    @Test
    public void testWorkWithInvalidDataTypeForName() {
        Employee employee = new Employee();
        employee.name = 123; // Invalid data type
        assertThrows(IllegalStateException.class, () -> {
            employee.work();
        });
    }

    @Test
    public void testWorkWithNullName() {
        Employee employee = new Employee();
        employee.name = null; // Null value
        assertThrows(NullPointerException.class, () -> {
            employee.work();
        });
    }

    @Test
    public void testWorkWithEmptyStringName() {
        Employee employee = new Employee();
        employee.name = ""; // Empty string
        assertThrows(IllegalArgumentException.class, () -> {
            employee.work();
        });
    }
}