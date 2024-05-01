using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FlashRedEffect : MonoBehaviour
{
    public float flashDuration = 0.5f; // Duration of the flash effect
    public Color flashColor = Color.red; // Color of the flash effect

    private float flashTimer = 0f; // Timer to control the flash effect
    private PostProcessVolume PostProcessVolume; // Reference to the Post-Processing Volume
    private ColorGrading colorGrading; // Reference to the ColorGrading effect

    void Start()
    {
        // Get the Post-Processing Volume component attached to the main camera
        PostProcessVolume = Camera.main.GetComponent<PostProcessVolume>();

        // Get the ColorGrading effect from the Post-Processing Volume
        if (PostProcessVolume != null)
        {
            PostProcessVolume.profile.TryGetSettings(out colorGrading);
        }
    }

    void Update()
    {
        // If flashTimer is greater than 0, decrease it over time
        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;

            // Calculate flash amount based on the timer
            float flashAmount = Mathf.Clamp01(flashTimer / flashDuration);

            // Update the color grading settings with the flash color
            if (colorGrading != null)
            {
                colorGrading.mixerRedOutRedIn.value = flashColor.r * flashAmount;
                colorGrading.mixerGreenOutGreenIn.value = flashColor.g * flashAmount;
                colorGrading.mixerBlueOutBlueIn.value = flashColor.b * flashAmount;
            }
        }
    }

    // Trigger the flash effect
    public void FlashScreen()
    {
        flashTimer = flashDuration;
    }
}