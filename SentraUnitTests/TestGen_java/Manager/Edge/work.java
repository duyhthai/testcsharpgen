import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class ManagerTest {

    @Test
    public void testWorkWithBoundaryMinValues() {
        Manager manager = new Manager();
        manager.name = "John";
        manager.teamSize = Integer.MIN_VALUE;
        assertThrows(IllegalArgumentException.class, () -> {
            manager.work();
        });
    }

    @Test
    public void testWorkWithBoundaryMaxValues() {
        Manager manager = new Manager();
        manager.name = "John";
        manager.teamSize = Integer.MAX_VALUE;
        assertThrows(IllegalArgumentException.class, () -> {
            manager.work();
        });
    }

    @Test
    public void testWorkWithNullName() {
        Manager manager = new Manager();
        manager.name = null;
        manager.teamSize = 5;
        assertThrows(NullPointerException.class, () -> {
            manager.work();
        });
    }
}