<?php

use PHPUnit\Framework\TestCase;

class DogTest extends TestCase
{
    public function testSpeakWithInvalidDataType()
    {
        $dog = new Dog();
        $dog->name = 123; // Invalid data type
        ob_start();
        $dog->speak();
        $output = ob_get_clean();
        $this->assertEquals("123 barks.\n", $output);
    }

    public function testSpeakWithNonStringData()
    {
        $dog = new Dog();
        $dog->name = true; // Non-string data
        ob_start();
        $dog->speak();
        $output = ob_get_clean();
        $this->assertEquals("1 barks.\n", $output); // Assuming boolean is converted to string "1"
    }

    public function testSpeakWithEmptyString()
    {
        $dog = new Dog();
        $dog->name = ""; // Empty string
        ob_start();
        $dog->speak();
        $output = ob_get_clean();
        $this->assertEquals(" barks.\n", $output); // Leading space due to empty string
    }
}
?>