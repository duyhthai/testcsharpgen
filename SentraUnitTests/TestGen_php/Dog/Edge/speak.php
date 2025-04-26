<?php

use PHPUnit\Framework\TestCase;

class DogTest extends TestCase
{
    public function testSpeakWithBoundaryMinLengthName()
    {
        $dog = new Dog();
        $dog->name = str_repeat('a', 1); // Minimum length name
        ob_start();
        $dog->speak();
        $output = ob_get_clean();
        $this->assertEquals("a barks.\n", $output);
    }

    public function testSpeakWithBoundaryMaxLengthName()
    {
        $dog = new Dog();
        $dog->name = str_repeat('a', 255); // Maximum length name
        ob_start();
        $dog->speak();
        $output = ob_get_clean();
        $this->assertEquals(str_repeat('a', 255) . " barks.\n", $output);
    }

    public function testSpeakWithEmptyStringName()
    {
        $dog = new Dog();
        $dog->name = '';
        ob_start();
        $dog->speak();
        $output = ob_get_clean();
        $this->assertEquals(" barks.\n", $output);
    }
}