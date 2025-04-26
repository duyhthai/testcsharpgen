import unittest

class TestWork(unittest.TestCase):
    def test_work_with_boundary_value(self):
        # Arrange
        input_value = sys.maxsize
        
        # Act
        result = work(input_value)
        
        # Assert
        self.assertEqual(result, expected_output)

    def test_work_with_negative_boundary_value(self):
        # Arrange
        input_value = -sys.maxsize - 1
        
        # Act
        result = work(input_value)
        
        # Assert
        self.assertEqual(result, expected_output)

    def test_work_with_zero(self):
        # Arrange
        input_value = 0
        
        # Act
        result = work(input_value)
        
        # Assert
        self.assertEqual(result, expected_output)

if __name__ == '__main__':
    unittest.main()