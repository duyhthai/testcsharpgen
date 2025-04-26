<?php

use PHPUnit\Framework\TestCase;

class AnimalTest extends TestCase
{
    public function testSpeakWithBoundaryMinLength()
    {
        $animal = new Animal();
        $animal->name = str_repeat('a', 1); // Minimum length name
        ob_start();
        $animal->speak();
        $output = ob_get_clean();
        $this->assertEquals("a makes a loud sound.\n", $output);
    }

    public function testSpeakWithBoundaryMaxLength()
    {
        $animal = new Animal();
        $animal->name = str_repeat('a', 255); // Maximum length name
        ob_start();
        $animal->speak();
        $output = ob_get_clean();
        $this->assertEquals(str_repeat('a', 255) . " makes a loud sound.\n", $output);
    }

    public function testSpeakWithEmptyString()
    {
        $animal = new Animal();
        $animal->name = '';
        ob_start();
        $animal->speak();
        $output = ob_get_clean();
        $this->assertEquals("\n", $output);
    }
}
?>