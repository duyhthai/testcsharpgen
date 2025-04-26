import unittest

class TestWork(unittest.TestCase):
    def test_throw_exception_on_unhandled_error(self):
        # Arrange
        work_instance = Work()

        # Act & Assert
        with self.assertRaises(Exception):
            work_instance.work()