<?php

use PHPUnit\Framework\TestCase;

class DogTest extends TestCase
{
    public function testSpeakThrowsExceptionIfNameNotSet()
    {
        $dog = new Dog();
        $this->expectException(Exception::class);
        $this->expectExceptionMessage('Name property is not set.');
        $dog->speak();
    }

    public function testSpeakThrowsExceptionIfNameIsNull()
    {
        $dog = new Dog();
        $dog->name = null;
        $this->expectException(Exception::class);
        $this->expectExceptionMessage('Name cannot be null.');
        $dog->speak();
    }

    public function testSpeakThrowsExceptionIfNameIsEmpty()
    {
        $dog = new Dog();
        $dog->name = '';
        $this->expectException(Exception::class);
        $this->expectExceptionMessage('Name cannot be empty.');
        $dog->speak();
    }
}
?>