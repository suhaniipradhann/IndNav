using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class NavigationUI : MonoBehaviour
{
public NavigationManager navManager;
public TMP_Dropdown destinationDropdown;
public TextMeshProUGUI statusText;
public Button navigateButton;
private void Start()
{
if (navigateButton != null)
navigateButton.interactable = false; // Disable initially
if (statusText != null)
statusText.text = "Select a destination to begin navigation.";
}
public void OnDestinationChanged()
{
if (destinationDropdown == null || navManager == null) return;
string selected = destinationDropdown.options.Count > destinationDropdown.value
? destinationDropdown.options[destinationDropdown.value].text
: "";
bool valid = destinationDropdown.value >= 0 && !string.IsNullOrEmpty(selected);
// Enable Navigate button only for valid selection
if (navigateButton != null)
navigateButton.interactable = valid;
// Update status text
if (statusText != null)
{
if (valid)
statusText.text = $"Selected: {selected}. Tap Navigate to proceed.";
else
statusText.text = "Please select a valid destination.";
}
}

public void OnNavigateButtonClicked()
{
if (navManager != null)
{
navManager.NavigateToSelected();
if (statusText != null)
statusText.text = "Navigation started!";
}
}
}