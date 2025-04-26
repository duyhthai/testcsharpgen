import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class ManagerTest {

    @Test
    public void testWorkWithNullName() {
        Manager manager = new Manager();
        manager.name = null;
        manager.teamSize = 5;
        assertThrows(NullPointerException.class, () -> manager.work());
    }

    @Test
    public void testWorkWithNegativeTeamSize() {
        Manager manager = new Manager();
        manager.name = "John";
        manager.teamSize = -3;
        assertThrows(IllegalArgumentException.class, () -> manager.work());
    }

    @Test
    public void testWorkWithLargeTeamSize() {
        Manager manager = new Manager();
        manager.name = "John";
        manager.teamSize = Integer.MAX_VALUE;
        assertThrows(IllegalArgumentException.class, () -> manager.work());
    }
}