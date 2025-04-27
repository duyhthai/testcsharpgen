<?php

use PHPUnit\Framework\TestCase;

class AnimalTest extends TestCase
{
    public function testSpeakWithInvalidDataType()
    {
        $animal = new Animal();
        $animal->name = 123; // Invalid data type
        ob_start();
        $animal->speak();
        $output = ob_get_clean();
        $this->assertStringContainsString('makes a loud sound.', $output);
    }

    public function testSpeakWithNonExistentProperty()
    {
        $animal = new Animal();
        unset($animal->name); // Non-existent property
        ob_start();
        $animal->speak();
        $output = ob_get_clean();
        $this->assertStringContainsString('makes a loud sound.', $output);
    }

    public function testSpeakWithEmptyString()
    {
        $animal = new Animal();
        $animal->name = ''; // Empty string
        ob_start();
        $animal->speak();
        $output = ob_get_clean();
        $this->assertStringContainsString('makes a loud sound.', $output);
    }
}