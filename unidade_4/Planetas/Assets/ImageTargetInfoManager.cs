using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ImageTargetInfoManager : MonoBehaviour
{
    public Text infoText; // Refer�ncia a um UI Text para exibir informa��es
    public GameObject lockedContent; // O conte�do associado � Image Target principal
    private string primaryImageTarget = ""; // Nome da Image Target principal atual
    private bool isLocked = false; // Indica se estamos travados na Image Target principal

    void Start()
    {
        // Certifique-se de que o texto est� vazio no in�cio
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
                // Caso n�o estejamos travados, definimos a Image Target principal
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
                // Caso j� estejamos travados, apenas exibe informa��es
                if (targetName != primaryImageTarget)
                {
                    Debug.Log($"Image Target Secund�ria Detectada: {targetName}");
                    if (infoText != null)
                    {
                        infoText.text = $"Informa��es de: {targetName}\n(Trocando n�o permitido)";
                    }
                }
            }
        }
        else if (status.Status == Status.NO_POSE)
        {
            // Limpa as informa��es da Image Target secund�ria se ela perder o tracking
            if (infoText != null && !string.IsNullOrEmpty(infoText.text))
            {
                infoText.text = $"Travado em: {primaryImageTarget}";
            }
        }
    }

    public void UnlockPrimaryTarget()
    {
        // Permite que o usu�rio "destrave" manualmente a Image Target principal
        isLocked = false;
        primaryImageTarget = "";
        if (infoText != null)
        {
            infoText.text = "Nenhuma Image Target Principal Travada.";
        }

        Debug.Log("Destravado. Pronto para selecionar uma nova Image Target principal.");
    }
}

