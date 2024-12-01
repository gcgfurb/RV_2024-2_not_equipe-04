using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ImageTargetInfoManager : MonoBehaviour
{
    public Text infoText; // Referência a um UI Text para exibir informações
    public GameObject lockedContent; // O conteúdo associado à Image Target principal
    private string primaryImageTarget = ""; // Nome da Image Target principal atual
    private bool isLocked = false; // Indica se estamos travados na Image Target principal

    void Start()
    {
        // Certifique-se de que o texto está vazio no início
        if (infoText != null)
        {
            infoText.text = "";
        }
    }

    public void OnTargetStatusChanged(ObserverBehaviour target, TargetStatus status)
    {
        if (status.Status == Status.TRACKED)
        {
            // Detecta o nome da Image Target
            string targetName = target.TargetName;

            if (!isLocked)
            {
                // Caso não estejamos travados, definimos a Image Target principal
                primaryImageTarget = targetName;
                isLocked = true;

                Debug.Log($"Image Target Principal Travada: {primaryImageTarget}");
                if (infoText != null)
                {
                    infoText.text = $"Travado em: {primaryImageTarget}";
                }
            }
            else
            {
                // Caso já estejamos travados, apenas exibe informações
                if (targetName != primaryImageTarget)
                {
                    Debug.Log($"Image Target Secundária Detectada: {targetName}");
                    if (infoText != null)
                    {
                        infoText.text = $"Informações de: {targetName}\n(Trocando não permitido)";
                    }
                }
            }
        }
        else if (status.Status == Status.NO_POSE)
        {
            // Limpa as informações da Image Target secundária se ela perder o tracking
            if (infoText != null && !string.IsNullOrEmpty(infoText.text))
            {
                infoText.text = $"Travado em: {primaryImageTarget}";
            }
        }
    }

    public void UnlockPrimaryTarget()
    {
        // Permite que o usuário "destrave" manualmente a Image Target principal
        isLocked = false;
        primaryImageTarget = "";
        if (infoText != null)
        {
            infoText.text = "Nenhuma Image Target Principal Travada.";
        }

        Debug.Log("Destravado. Pronto para selecionar uma nova Image Target principal.");
    }
}

