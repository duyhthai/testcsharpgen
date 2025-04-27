import unittest

class TestWork(unittest.TestCase):
    def setUp(self):
        self.worker = Worker("John")

    def test_work_with_invalid_name_type(self):
        with self.assertRaises(TypeError):
            self.worker.name = 123
            self.worker.work()

    def test_work_with_empty_name(self):
        with self.assertRaises(ValueError):
            self.worker.name = ""
            self.worker.work()

    def test_work_with_none_name(self):
        with self.assertRaises(ValueError):
            self.worker.name = None
            self.worker.work()

if __name__ == '__main__':
    unittest.main()