<?php

use PHPUnit\Framework\TestCase;

class DogTest extends TestCase
{
    public function testSpeakWithValidName()
    {
        $dog = new Dog();
        $dog->name = "Buddy";
        ob_start();
        $dog->speak();
        $output = ob_get_clean();
        $this->assertEquals("Buddy barks.\n", $output);
    }

    public function testSpeakWithDifferentName()
    {
        $dog = new Dog();
        $dog->name = "Max";
        ob_start();
        $dog->speak();
        $output = ob_get_clean();
        $this->assertEquals("Max barks.\n", $output);
    }

    public function testSpeakWithLongerName()
    {
        $dog = new Dog();
        $dog->name = "Buddy the Great";
        ob_start();
        $dog->speak();
        $output = ob_get_clean();
        $this->assertEquals("Buddy the Great barks.\n", $output);
    }
}
?>