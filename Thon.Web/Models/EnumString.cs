namespace Thon.Web.Models;

public abstract class EnumString(string value)
{
    public string Value => value;

    public static bool operator ==(EnumString e1, string? e2) => e1.Value == e2;
    public static bool operator !=(EnumString e1, string? e2) => !(e1 == e2);

    public static bool operator ==(string? e1, EnumString e2) => e2 == e1;
    public static bool operator !=(string? e1, EnumString e2) => e2 != e1;

    public static bool operator ==(EnumString e1, EnumString e2) => e1.Value == e2.Value;
    public static bool operator !=(EnumString e1, EnumString e2) => !(e1 == e2);

    public override bool Equals(object? obj)
    {
        if (obj is not null)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is EnumString enumString)
                return Value.Equals(enumString.Value);
        }

        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;

    public static implicit operator string(EnumString d) => d.Value;
}
