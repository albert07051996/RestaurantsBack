using BuildingBlocks.Shared.Domain;

namespace Product.Domain.Entities;

public class RestaurantSettings : BaseEntity
{
    public string RestaurantName { get; private set; } = string.Empty;
    public string RestaurantNameEn { get; private set; } = string.Empty;
    public string TaglineKa { get; private set; } = string.Empty;
    public string TaglineEn { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string BackgroundImageUrl { get; private set; } = string.Empty;
    public string LogoUrl { get; private set; } = string.Empty;
    public string OpeningHours { get; private set; } = string.Empty;
    public string PrimaryColor { get; private set; } = "#d4af37";
    public string SecondaryColor { get; private set; } = "#c19a6b";
    public bool IsActive { get; private set; }

    private RestaurantSettings() { }

    public RestaurantSettings(
        string restaurantName,
        string restaurantNameEn,
        string taglineKa,
        string taglineEn,
        string address,
        string phone,
        string email)
    {
        RestaurantName = restaurantName;
        RestaurantNameEn = restaurantNameEn;
        TaglineKa = taglineKa;
        TaglineEn = taglineEn;
        Address = address;
        Phone = phone;
        Email = email;
        IsActive = true;
    }

    public void Update(
        string restaurantName,
        string restaurantNameEn,
        string taglineKa,
        string taglineEn,
        string address,
        string phone,
        string email,
        string openingHours)
    {
        RestaurantName = restaurantName;
        RestaurantNameEn = restaurantNameEn;
        TaglineKa = taglineKa;
        TaglineEn = taglineEn;
        Address = address;
        Phone = phone;
        Email = email;
        OpeningHours = openingHours;
        UpdateTimestamp();
    }

    public void UpdateBackgroundImage(string backgroundImageUrl)
    {
        BackgroundImageUrl = backgroundImageUrl;
        UpdateTimestamp();
    }

    public void UpdateLogo(string logoUrl)
    {
        LogoUrl = logoUrl;
        UpdateTimestamp();
    }

    public void UpdateColors(string primaryColor, string secondaryColor)
    {
        PrimaryColor = primaryColor;
        SecondaryColor = secondaryColor;
        UpdateTimestamp();
    }

    public void Activate()
    {
        IsActive = true;
        UpdateTimestamp();
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdateTimestamp();
    }
}
