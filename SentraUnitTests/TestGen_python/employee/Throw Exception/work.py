import unittest

class TestWork(unittest.TestCase):
    def setUp(self):
        self.worker = Worker("John")

    def test_work_with_none_name(self):
        with self.assertRaises(TypeError):
            worker = Worker(None)
            worker.work()

    def test_work_with_empty_string_name(self):
        with self.assertRaises(ValueError):
            worker = Worker("")
            worker.work()

    def test_work_with_invalid_name_type(self):
        with self.assertRaises(TypeError):
            worker = Worker(123)
            worker.work()

class Worker:
    def __init__(self, name):
        if not isinstance(name, str):
            raise TypeError("Name must be a string")
        if not name.strip():
            raise ValueError("Name cannot be empty")
        self.name = name

    def work(self):
        print(f"{self.name} is working.")

if __name__ == '__main__':
    unittest.main()