import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class ManagerTest {

    @Test
    public void testWorkWithNullNameThrowsException() {
        Manager manager = new Manager();
        manager.name = null;
        manager.teamSize = 5;

        assertThrows(NullPointerException.class, () -> {
            manager.work();
        });
    }

    @Test
    public void testWorkWithNegativeTeamSizeThrowsException() {
        Manager manager = new Manager();
        manager.name = "John";
        manager.teamSize = -3;

        assertThrows(IllegalArgumentException.class, () -> {
            manager.work();
        });
    }

    @Test
    public void testWorkWithLargeTeamSizeThrowsException() {
        Manager manager = new Manager();
        manager.name = "John";
        manager.teamSize = Integer.MAX_VALUE;

        assertThrows(IllegalArgumentException.class, () -> {
            manager.work();
        });
    }
}