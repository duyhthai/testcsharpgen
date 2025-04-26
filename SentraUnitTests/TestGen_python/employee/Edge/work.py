import unittest

class TestWork(unittest.TestCase):
    def setUp(self):
        self.worker = Worker("John")

    def test_work_with_boundary_min_length_name(self):
        worker = Worker("A")
        worker.work()
        # Assuming print statement captures output
        captured_output = io.StringIO()
        sys.stdout = captured_output
        worker.work()
        sys.stdout = sys.__stdout__
        self.assertEqual(captured_output.getvalue(), "A is working.\n")

    def test_work_with_boundary_max_length_name(self):
        worker = Worker("a" * 100)
        worker.work()
        captured_output = io.StringIO()
        sys.stdout = captured_output
        worker.work()
        sys.stdout = sys.__stdout__
        self.assertEqual(captured_output.getvalue(), ("a" * 100) + " is working.\n")

    def test_work_with_empty_string_name(self):
        worker = Worker("")
        worker.work()
        captured_output = io.StringIO()
        sys.stdout = captured_output
        worker.work()
        sys.stdout = sys.__stdout__
        self.assertEqual(captured_output.getvalue(), "\n")

if __name__ == '__main__':
    unittest.main()