import org.junit.jupiter.api.Test;
import static org.mockito.Mockito.*;

public class ManagerTest {

    @Test
    public void testWorkWithValidInputs() {
        // Arrange
        Manager manager = new Manager();
        manager.name = "John";
        manager.teamSize = 10;

        // Act
        manager.work();

        // Assert
        assertEquals("John is managing a team of 10 employees.", System.out.toString());
    }

    @Test
    public void testWorkWithEmptyName() {
        // Arrange
        Manager manager = new Manager();
        manager.name = "";
        manager.teamSize = 5;

        // Act
        manager.work();

        // Assert
        assertEquals(" is managing a team of 5 employees.", System.out.toString());
    }

    @Test
    public void testWorkWithZeroTeamSize() {
        // Arrange
        Manager manager = new Manager();
        manager.name = "Jane";
        manager.teamSize = 0;

        // Act
        manager.work();

        // Assert
        assertEquals("Jane is managing a team of 0 employees.", System.out.toString());
    }
}