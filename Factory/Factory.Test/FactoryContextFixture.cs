using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit;

namespace Factory.Test
{
    /// <summary>
    /// ��������: ������ ��������, ������������� ����� �������. 
    /// � ������������ ������ ��� ����������� ������.
    /// </summary>
    public class FactoryContextFixture
    {
        public List<TypeIndustry> Types { get; }
        public List<OwnershipForm> Ownerships { get; }
        public List<Supply> Supplies { get; }
        public List<Enterprise> Enterprises { get; }
        public List<Supplier> Suppliers { get; }

        public FactoryContextFixture()
        {
            // ������ ����� ���������
            Types = new List<TypeIndustry>()
            {
                new TypeIndustry(1, "C������� ���������"),
                new TypeIndustry(2, "���������"),
                new TypeIndustry(3, "������ ��������������"),
                new TypeIndustry(4, "������� ��������������"),
                new TypeIndustry(5, "�������������"),
                new TypeIndustry(6, "����������� - ����������� ���������")
            };

            // ������ ���� �������������
            Ownerships = new List<OwnershipForm>()
            {
                new OwnershipForm(1, "���"),
                new OwnershipForm(2, "���"),
                new OwnershipForm(3, "��"),
                new OwnershipForm(4, "���")
            };

            // ������ ��������
            Supplies = new List<Supply>()
            {
                new Supply(1, 1, 1, "20.01.2023", 3),  // ���� - �����
                new Supply(2, 1, 2, "31.10.2022", 5),  // ���� - �������
                new Supply(3, 3, 3, "14.08.2022", 1),  // ���� - �����
                new Supply(4, 4, 4, "05.02.2023", 10), // ������� - ����
                new Supply(5, 2, 5, "27.02.2023", 6),  // ��� - �����
                new Supply(6, 5, 5, "13.01.2023", 2),  // ����� - �����
                new Supply(7, 4, 3, "04.01.2023", 12), // ������� - �����
                new Supply(8, 2, 2, "09.12.2022", 4)   // ��� - �������
            };

            // ������ �����������
            Enterprises = new List<Enterprise>()
            {
                new Enterprise(1, "1036300446093", 6, "����",    "��.22 ��������� �.7�",  "88469926984",   1, 100, 1000.0),
                new Enterprise(2, "1156313028981", 6, "���",     "��.22 ��������� �.10�", "88462295931",   2, 150, 1500.0),
                new Enterprise(3, "1116318009510", 4, "����",    "��.����������� �.6�",   "884692007711",  2, 200, 2000.0),
                new Enterprise(4, "1026300767899", 2, "�������", "��.������ �.32",        "88463720888",   3, 250, 2500.0),
                new Enterprise(5, "1026301697487", 6, "�����",   "��.������ �.24",        "88469983785",   4, 130, 1300.0),
            };

            // ������ �����������
            Suppliers = new List<Supplier>()
            {
                new Supplier(1, "����� ��������",  "��. ����������� �.42",    "89375550203"),
                new Supplier(2, "������� ����",   "��. ����������� �.1",     "89370101010"),
                new Supplier(3, "����� �������",  "��. ���������� �.50",     "89376431289"),
                new Supplier(4, "���� ����",      "��. ������������� �.35",  "89372229978"),
                new Supplier(5, "����� �������",  "��. �������� �.14",       "89371234567")
            };
        }
    }
}