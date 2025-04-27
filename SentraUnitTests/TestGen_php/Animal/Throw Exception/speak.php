<?php

use PHPUnit\Framework\TestCase;

class AnimalTest extends TestCase
{
    /**
     * @expectedException Exception
     */
    public function testSpeakWithoutName()
    {
        $animal = new Animal();
        unset($animal->name);
        $animal->speak();
    }

    /**
     * @expectedException Exception
     */
    public function testSpeakWithNullName()
    {
        $animal = new Animal();
        $animal->name = null;
        $animal->speak();
    }

    /**
     * @expectedException Exception
     */
    public function testSpeakWithEmptyName()
    {
        $animal = new Animal();
        $animal->name = '';
        $animal->speak();
    }
}
?>