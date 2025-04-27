<?php

use PHPUnit\Framework\TestCase;

class AnimalTest extends TestCase
{
    public function testSpeakWithValidName()
    {
        $animal = new Animal();
        $animal->name = "Lion";
        ob_start();
        $animal->speak();
        $output = ob_get_clean();
        $this->assertEquals("Lion makes a loud sound.\n", $output);
    }

    public function testSpeakWithEmptyName()
    {
        $animal = new Animal();
        $animal->name = "";
        ob_start();
        $animal->speak();
        $output = ob_get_clean();
        $this->assertEquals("\n", $output);
    }

    public function testSpeakWithNullName()
    {
        $animal = new Animal();
        $animal->name = null;
        ob_start();
        $animal->speak();
        $output = ob_get_clean();
        $this->assertEquals("\n", $output);
    }
}
?>