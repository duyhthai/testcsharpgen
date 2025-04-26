import unittest

class TestWork(unittest.TestCase):
    def test_work_with_invalid_input_type(self):
        with self.assertRaises(TypeError):
            work(123)

    def test_work_with_empty_input(self):
        with self.assertRaises(ValueError):
            work("")

    def test_work_with_null_input(self):
        with self.assertRaises(TypeError):
            work(None)