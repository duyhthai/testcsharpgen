import unittest

class TestWork(unittest.TestCase):
    def setUp(self):
        self.worker = Worker("John")

    def test_work_with_valid_name(self):
        # Arrange
        expected_output = "John is working."

        # Act
        actual_output = capture_output(lambda: self.worker.work())

        # Assert
        self.assertEqual(actual_output, expected_output)

def capture_output(func):
    import io
    import sys
    old_stdout = sys.stdout
    new_stdout = io.StringIO()
    sys.stdout = new_stdout
    func()
    sys.stdout = old_stdout
    return new_stdout.getvalue()

class Worker:
    def __init__(self, name):
        self.name = name

    def work(self):
        print(f"{self.name} is working.")

# Run the tests
if __name__ == '__main__':
    unittest.main()