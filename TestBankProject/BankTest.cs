﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBankProject
{
    internal class BankTest
    {
        Bank b;

        [SetUp]
        public void Setup()
        {
            b = new Bank();
            b.UjSzamla("Gipsz Jakab", "1234");
        }

        [Test]
        public void UjSzamlaEgyenleg0()
        {
            Assert.That(b.Egyenleg("1234"), Is.Zero);
        }

        [Test]
        public void UjSzamlaNullNev()
        {
            Assert.Throws<ArgumentNullException>(() => b.UjSzamla(null, "4321"));
        }

        [Test]
        public void UjSzamlaNullSzamlaszam()
        {
            Assert.Throws<ArgumentNullException>(() => b.UjSzamla("Teszt Elek", null));
        }

        [Test]
        public void UjSzamlaUresNev()
        {
            Assert.Throws<ArgumentException>(() => b.UjSzamla("", "4321"));
        }

        [Test]
        public void UjSzamlaUresSzamlaszam()
        {
            Assert.Throws<ArgumentException>(() => b.UjSzamla("Teszt Elek", ""));
        }

        [Test]
        public void UjSzamlaLetezoSzamlaszammal()
        {
            Assert.Throws<ArgumentException>(() => b.UjSzamla("Teszt Elek", "1234"));
        }

        [Test]
        public void UjSzamlaLetezoNevvel()
        {
            Assert.DoesNotThrow(() => b.UjSzamla("Gipsz Jakab", "4321"));
        }

        [Test]
        public void Egyenleg_NullSzamlaszam()
        {
            Assert.Throws<ArgumentNullException>(() => b.Egyenleg(null));
        }

        [Test]
        public void Egyenleg_UresSzamlaszam()
        {
            Assert.Throws<ArgumentException>(() => b.Egyenleg(""));
        }

        [Test]
        public void Egyenleg_NemLetezoSzamlaszam()
        {
            Assert.Throws<HibasSzamlaszamException>(() => b.Egyenleg("4321"));
        }


        [Test]
        public void EgyenlegFeltolt_NullSzamlaszam()
        {
            Assert.Throws<ArgumentNullException>(() => b.EgyenlegFeltolt(null, 10000));
        }

        [Test]
        public void EgyenlegFeltolt_UresSzamlaszam()
        {
            Assert.Throws<ArgumentException>(() => b.EgyenlegFeltolt("", 10000));
        }

        [Test]
        public void EgyenlegFeltolt_NemLetezoSzamlaszam()
        {
            Assert.Throws<HibasSzamlaszamException>(() => b.EgyenlegFeltolt("4321", 10000));
        }

        [Test]
        public void EgyenlegFeltolt_0Osszeg()
        {
            Assert.Throws<ArgumentException>(() => b.EgyenlegFeltolt("1234", 0));
        }

        [Test]
        public void EgyenlegFeltolt_OsszegMegvaltozik()
        {
            b.EgyenlegFeltolt("1234", 10000);
            Assert.That(b.Egyenleg("1234"), Is.EqualTo(10000));
        }

        [Test]
        public void EgyenlegFeltolt_OsszegHozzaadodik()
        {
            b.EgyenlegFeltolt("1234", 10000);
            Assert.That(b.Egyenleg("1234"), Is.EqualTo(10000));
            b.EgyenlegFeltolt("1234", 20000);
            Assert.That(b.Egyenleg("1234"), Is.EqualTo(30000));
        }

        [Test]
        public void EgyenlegFeltolt_JoSzamlaraKerulAzOsszeg()
        {
            b.UjSzamla("Teszt Elek", "4321");
            b.UjSzamla("Gipsz Jakab", "5678");
            b.EgyenlegFeltolt("1234", 10000);
            Assert.That(b.Egyenleg("1234"), Is.EqualTo(10000));
            b.EgyenlegFeltolt("4321", 20000);
            Assert.That(b.Egyenleg("4321"), Is.EqualTo(20000));
            Assert.That(b.Egyenleg("5678"), Is.Zero);
        }


    }
}
