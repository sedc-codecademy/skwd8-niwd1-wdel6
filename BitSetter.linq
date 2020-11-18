<Query Kind="Statements" />

byte value = 143;

if ((value & 0b1000_0000) == 0b1000_0000) 
{
	"THE FIRST BIT IS SET".Dump();
}

if ((value & 0b0100_0000) == 0b0100_0000) 
{
	"THE SECOND BIT IS SET".Dump();
}

if ((value & 32) == 32) 
{
	"THE THIRD BIT IS SET".Dump();
}
if ((value & 16) == 16) 
{
	"THE FOURTH BIT IS SET".Dump();
}
if ((value & 8) == 8) 
{
	"THE FIFTH BIT IS SET".Dump();
}
if ((value & 4) == 4) 
{
	"THE SIXTH BIT IS SET".Dump();
}
if ((value & 2) == 2) 
{
	"THE SEVENTH BIT IS SET".Dump();
}
if ((value & 1) == 1) 
{
	"THE EIGHTH BIT IS SET".Dump();
}