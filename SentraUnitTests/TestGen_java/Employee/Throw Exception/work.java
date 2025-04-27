import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertThrows;

public class EmployeeTest {

    @Test
    public void testWorkWithNullName() {
        Employee employee = new Employee();
        employee.name = null;
        assertThrows(NullPointerException.class, () -> employee.work());
    }

    @Test
    public void testWorkWithEmptyName() {
        Employee employee = new Employee();
        employee.name = "";
        assertThrows(IllegalArgumentException.class, () -> employee.work());
    }

    @Test
    public void testWorkWithInvalidDataTypeForName() {
        Employee employee = new Employee();
        employee.name = 123; // Assuming name should be a String
        assertThrows(ClassCastException.class, () -> employee.work());
    }
}