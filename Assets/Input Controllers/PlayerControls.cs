// GENERATED AUTOMATICALLY FROM 'Assets/Input Controllers/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerActions"",
            ""id"": ""18561a40-8161-4d76-b624-7b9893a3bd3b"",
            ""actions"": [
                {
                    ""name"": ""MoveBinds"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e90c0815-e114-455e-b474-1d22b51f1f2e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""45f850ea-e7f5-4422-bf2c-82b6ab19ae29"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveMira"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f9f438ab-368d-467c-b095-c33c35b56892"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DisparoPrimario"",
                    ""type"": ""Button"",
                    ""id"": ""1db9a11b-5805-40cc-bae2-3bba0f607e73"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DisparoSecundario"",
                    ""type"": ""Button"",
                    ""id"": ""257744be-279b-438a-967d-cca827aa5d60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ApuntarMira"",
                    ""type"": ""Button"",
                    ""id"": ""1ea45208-c84f-4449-aa08-11994092ecef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accion"",
                    ""type"": ""Button"",
                    ""id"": ""78944cf8-2970-483d-8b7a-e521f85008dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RestarPartida"",
                    ""type"": ""Button"",
                    ""id"": ""442766d9-82c0-4883-91b4-3fae51476576"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShopOpen"",
                    ""type"": ""Button"",
                    ""id"": ""0e07b2ae-ec7f-4e69-a7a7-bdb627b1674f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BuildOpen"",
                    ""type"": ""Button"",
                    ""id"": ""24690792-d722-475c-aee0-52d8315fff85"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pausar"",
                    ""type"": ""Button"",
                    ""id"": ""f439235d-21fa-4fe3-871c-e974c356a54e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dialogo"",
                    ""type"": ""Button"",
                    ""id"": ""2368caec-8422-42f7-b693-3ccb6510f6b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StatsShown"",
                    ""type"": ""Button"",
                    ""id"": ""3ca2ba6e-5097-48db-a9a2-a21c199961e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UseBandage"",
                    ""type"": ""Button"",
                    ""id"": ""ddb521d5-f8d1-47c4-b20d-33eaff78f341"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Move2DValue"",
                    ""id"": ""4ab7eed9-6203-4ed0-a0db-7bd985325125"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBinds"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9821ee36-587c-43e5-adf6-cf6f197f4ab6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""MoveBinds"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""716fa542-61e4-4da2-b8aa-f9458fbe6f40"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""MoveBinds"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2072e1f2-3a03-4bf0-9705-4bd7331978d4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""MoveBinds"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9bc6cfd0-4cd2-478d-bd80-9b6d11751311"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""MoveBinds"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b6f9089f-fb95-4f62-94a2-04f975c390da"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""MoveBinds"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1501e8c1-030f-4f33-bd3f-7dded792c308"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc22f05e-6a11-4b97-85b9-dfe63f18fec0"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94a0559c-04fd-49ac-906b-82008aa70545"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""MoveMira"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b1e3333-1b83-49a4-9871-585ce9aa594b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""MoveMira"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fc2eec8-f54b-432b-8d05-f50d8c0f4977"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""ApuntarMira"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e681e7a2-cb25-4ece-bfe9-c7dc5fc92e51"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""ApuntarMira"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c89a20d-322e-4760-b397-b4daa49315e1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""DisparoPrimario"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25ae7229-b43c-4c04-b88a-2c52a1cf623d"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""DisparoPrimario"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c7a6030-6cf8-4c83-8234-9f389183a2fb"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""Accion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17fa9612-2dc1-4c3f-aec3-bec42f1937f6"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Accion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""737f9fc6-e6fc-4502-ad9e-d35c5633e16d"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""RestarPartida"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23033bb1-dd80-4c0e-8516-ebb29c19e160"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""RestarPartida"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b29d5434-508b-4acc-aa5c-c57896a98271"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""ShopOpen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c18a1efb-6a68-4915-8a30-600aa39e7904"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""ShopOpen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""196b0733-4f45-47cd-bb6a-4b72f08200a0"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""BuildOpen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7810739-08f8-45e2-8b1c-f3174f54c7ec"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""Pausar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f62d203-dff7-4811-b572-bf798bc310b0"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Pausar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18c73b2f-cbe6-46e8-bee8-22a8da085367"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""Dialogo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""93985d47-c679-4cd1-9f0b-4a6285663cf5"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Dialogo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ea03e15-4ee6-4c62-b063-7b869d3a5eba"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""DisparoSecundario"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f316e468-85fe-435e-ba9a-87284bb3ba5a"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""DisparoSecundario"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f301b67-4a74-4d03-9272-355498ed8e60"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""StatsShown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""316d10a7-d804-4e43-933d-774ff77faf81"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""StatsShown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0009416-92f9-44f6-89d2-72959b9a4274"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""UseBandage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b8e6100-e166-4f58-b2e1-a2a06a315ccd"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""UseBandage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51d266bb-bd1a-40ef-a4fc-cfdf7bd4d333"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""BuildOpen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""e4a761f9-f569-4983-b629-2067af47e7f4"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c2e56cb9-d9a2-4f38-a793-83376904890e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""efb05687-76f7-4500-9dde-bda9615a6ba6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""927789dc-3e9b-4fda-a1ef-d1d294dbd43f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5132aaf6-96ee-4f41-8d05-80cd4cda0c3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cf6bc40b-f89c-4cb0-9c76-ed7fbd7cd193"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShopClose"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5b0eea0b-ede5-41dc-b9a7-ed0c01d860cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BuildClose"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8a7fd4e0-4fef-4fd5-b9d2-2e0e19058258"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pausar"",
                    ""type"": ""Button"",
                    ""id"": ""7a401992-6ec3-4888-b680-03b083d1ca54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CloseUI"",
                    ""type"": ""Button"",
                    ""id"": ""1ef94eda-6e36-439c-aec5-0abba58b0526"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""dab71af3-dee8-455d-95c0-639515713edd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0a9fa456-93aa-4700-8ce8-d0bb0a69671a"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Control"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c144decd-4ed4-4d1a-9096-0a3dc72b613e"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Control"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3e212a27-3b84-4905-bd6d-d3175aa92fa0"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Control"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""95406399-4425-4799-b55d-928e736b5461"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Control"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""b61b0c3a-a379-434a-9aef-57ddb7593d0f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""18db3569-30a8-4b82-b0e0-121bea021c5f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""efea47f4-49ed-41c2-899e-5049063e036d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""288f88a9-30fb-4b09-8cb5-9cb68e4d446f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c7a8fb45-3379-41a4-b247-c9dd24079d0b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""09efcbe2-ed71-48ba-be9f-5af00fa46568"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Teclado"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66eeb4a9-02f6-44ee-bc28-d34b9669a28e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Teclado"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3c1614e-f085-4e39-82ce-cb6e9fd49e63"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Teclado"",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc6153b6-b945-42ff-bfc1-306ce7207568"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""ShopClose"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94b8200c-3cc7-4fcd-a9db-c246b77d0baf"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""ShopClose"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f9471db-c8a2-48d5-aef9-e06e3ba18b08"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c803422d-825c-4860-8eeb-af9c3e1e4284"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""BuildClose"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9543080-35a7-4471-848a-d6fa82d304b1"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""Pausar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e5a301b-0925-4367-b67f-8bc1f96fbc72"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Pausar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee694489-a021-413b-a786-897eba191f01"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""CloseUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ceb27eb5-2e25-41e2-94ce-ee819ac65544"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""BuildClose"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Construction"",
            ""id"": ""1b9d8a12-0e7b-4b64-9cc6-db754b97e549"",
            ""actions"": [
                {
                    ""name"": ""Movimiento"",
                    ""type"": ""PassThrough"",
                    ""id"": ""92c295d4-85f9-42cd-bd8a-e99ac4d91d9a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BuildCancel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2346bcaa-d32e-47a2-b075-17ef23ff8005"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlaceBuild"",
                    ""type"": ""Button"",
                    ""id"": ""53424553-a4bc-40e5-a1fa-306c6673e1fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pausar"",
                    ""type"": ""Button"",
                    ""id"": ""7b2fef3a-a968-4b13-b553-2bf13d015331"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CloseUI"",
                    ""type"": ""Button"",
                    ""id"": ""1e1109d0-ec44-42ef-9be5-9bd81ed969a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8773de35-8026-4ed6-b6ec-b57557e9364c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""BuildCancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""384b5a72-9659-4529-ac14-9196859cad7a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""BuildCancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29abfa82-23a2-4829-a0f9-be8f18be3820"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""Movimiento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ControlArrows"",
                    ""id"": ""d7b4a716-35a0-4f30-bf35-0040bc8b8d81"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movimiento"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c1207b0e-022d-4a67-85d1-7a50fe584065"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Movimiento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0d539f96-cb83-4553-bdd7-e956c99cefde"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Movimiento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""eaff956c-bfd6-4736-b243-231bdab87d0e"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Movimiento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""48f840e9-fdd3-4872-b2ed-1503b8db0990"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Movimiento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8d72a942-2d8b-4972-ba07-f17b0d169b6c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""PlaceBuild"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0636e2e3-fb59-4769-8d53-77a2abe23348"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""PlaceBuild"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7269fd23-da15-4550-8314-1c86a434ee77"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""Pausar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dcfaf906-4c75-4fe2-96c5-17aedaace4f9"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Pausar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7eb5060c-f3e0-4038-bf8b-25498e7c5e51"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Teclado"",
                    ""action"": ""CloseUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""NoControles"",
            ""id"": ""f79e4c8a-c9e6-4bcb-873f-f06050adf158"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""b0a9730e-db4a-42c6-9945-f63423c5d471"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e18f2e3e-6f65-4a38-b8a9-92f554a7e8b8"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Teclado"",
            ""bindingGroup"": ""Teclado"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Control"",
            ""bindingGroup"": ""Control"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_MoveBinds = m_PlayerActions.FindAction("MoveBinds", throwIfNotFound: true);
        m_PlayerActions_Dash = m_PlayerActions.FindAction("Dash", throwIfNotFound: true);
        m_PlayerActions_MoveMira = m_PlayerActions.FindAction("MoveMira", throwIfNotFound: true);
        m_PlayerActions_DisparoPrimario = m_PlayerActions.FindAction("DisparoPrimario", throwIfNotFound: true);
        m_PlayerActions_DisparoSecundario = m_PlayerActions.FindAction("DisparoSecundario", throwIfNotFound: true);
        m_PlayerActions_ApuntarMira = m_PlayerActions.FindAction("ApuntarMira", throwIfNotFound: true);
        m_PlayerActions_Accion = m_PlayerActions.FindAction("Accion", throwIfNotFound: true);
        m_PlayerActions_RestarPartida = m_PlayerActions.FindAction("RestarPartida", throwIfNotFound: true);
        m_PlayerActions_ShopOpen = m_PlayerActions.FindAction("ShopOpen", throwIfNotFound: true);
        m_PlayerActions_BuildOpen = m_PlayerActions.FindAction("BuildOpen", throwIfNotFound: true);
        m_PlayerActions_Pausar = m_PlayerActions.FindAction("Pausar", throwIfNotFound: true);
        m_PlayerActions_Dialogo = m_PlayerActions.FindAction("Dialogo", throwIfNotFound: true);
        m_PlayerActions_StatsShown = m_PlayerActions.FindAction("StatsShown", throwIfNotFound: true);
        m_PlayerActions_UseBandage = m_PlayerActions.FindAction("UseBandage", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Navigate = m_UI.FindAction("Navigate", throwIfNotFound: true);
        m_UI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
        m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
        m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
        m_UI_ScrollWheel = m_UI.FindAction("ScrollWheel", throwIfNotFound: true);
        m_UI_ShopClose = m_UI.FindAction("ShopClose", throwIfNotFound: true);
        m_UI_BuildClose = m_UI.FindAction("BuildClose", throwIfNotFound: true);
        m_UI_Pausar = m_UI.FindAction("Pausar", throwIfNotFound: true);
        m_UI_CloseUI = m_UI.FindAction("CloseUI", throwIfNotFound: true);
        // Construction
        m_Construction = asset.FindActionMap("Construction", throwIfNotFound: true);
        m_Construction_Movimiento = m_Construction.FindAction("Movimiento", throwIfNotFound: true);
        m_Construction_BuildCancel = m_Construction.FindAction("BuildCancel", throwIfNotFound: true);
        m_Construction_PlaceBuild = m_Construction.FindAction("PlaceBuild", throwIfNotFound: true);
        m_Construction_Pausar = m_Construction.FindAction("Pausar", throwIfNotFound: true);
        m_Construction_CloseUI = m_Construction.FindAction("CloseUI", throwIfNotFound: true);
        // NoControles
        m_NoControles = asset.FindActionMap("NoControles", throwIfNotFound: true);
        m_NoControles_Newaction = m_NoControles.FindAction("New action", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_MoveBinds;
    private readonly InputAction m_PlayerActions_Dash;
    private readonly InputAction m_PlayerActions_MoveMira;
    private readonly InputAction m_PlayerActions_DisparoPrimario;
    private readonly InputAction m_PlayerActions_DisparoSecundario;
    private readonly InputAction m_PlayerActions_ApuntarMira;
    private readonly InputAction m_PlayerActions_Accion;
    private readonly InputAction m_PlayerActions_RestarPartida;
    private readonly InputAction m_PlayerActions_ShopOpen;
    private readonly InputAction m_PlayerActions_BuildOpen;
    private readonly InputAction m_PlayerActions_Pausar;
    private readonly InputAction m_PlayerActions_Dialogo;
    private readonly InputAction m_PlayerActions_StatsShown;
    private readonly InputAction m_PlayerActions_UseBandage;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveBinds => m_Wrapper.m_PlayerActions_MoveBinds;
        public InputAction @Dash => m_Wrapper.m_PlayerActions_Dash;
        public InputAction @MoveMira => m_Wrapper.m_PlayerActions_MoveMira;
        public InputAction @DisparoPrimario => m_Wrapper.m_PlayerActions_DisparoPrimario;
        public InputAction @DisparoSecundario => m_Wrapper.m_PlayerActions_DisparoSecundario;
        public InputAction @ApuntarMira => m_Wrapper.m_PlayerActions_ApuntarMira;
        public InputAction @Accion => m_Wrapper.m_PlayerActions_Accion;
        public InputAction @RestarPartida => m_Wrapper.m_PlayerActions_RestarPartida;
        public InputAction @ShopOpen => m_Wrapper.m_PlayerActions_ShopOpen;
        public InputAction @BuildOpen => m_Wrapper.m_PlayerActions_BuildOpen;
        public InputAction @Pausar => m_Wrapper.m_PlayerActions_Pausar;
        public InputAction @Dialogo => m_Wrapper.m_PlayerActions_Dialogo;
        public InputAction @StatsShown => m_Wrapper.m_PlayerActions_StatsShown;
        public InputAction @UseBandage => m_Wrapper.m_PlayerActions_UseBandage;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @MoveBinds.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMoveBinds;
                @MoveBinds.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMoveBinds;
                @MoveBinds.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMoveBinds;
                @Dash.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDash;
                @MoveMira.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMoveMira;
                @MoveMira.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMoveMira;
                @MoveMira.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMoveMira;
                @DisparoPrimario.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDisparoPrimario;
                @DisparoPrimario.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDisparoPrimario;
                @DisparoPrimario.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDisparoPrimario;
                @DisparoSecundario.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDisparoSecundario;
                @DisparoSecundario.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDisparoSecundario;
                @DisparoSecundario.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDisparoSecundario;
                @ApuntarMira.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnApuntarMira;
                @ApuntarMira.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnApuntarMira;
                @ApuntarMira.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnApuntarMira;
                @Accion.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAccion;
                @Accion.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAccion;
                @Accion.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAccion;
                @RestarPartida.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRestarPartida;
                @RestarPartida.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRestarPartida;
                @RestarPartida.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRestarPartida;
                @ShopOpen.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShopOpen;
                @ShopOpen.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShopOpen;
                @ShopOpen.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShopOpen;
                @BuildOpen.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnBuildOpen;
                @BuildOpen.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnBuildOpen;
                @BuildOpen.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnBuildOpen;
                @Pausar.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPausar;
                @Pausar.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPausar;
                @Pausar.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPausar;
                @Dialogo.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDialogo;
                @Dialogo.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDialogo;
                @Dialogo.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDialogo;
                @StatsShown.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnStatsShown;
                @StatsShown.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnStatsShown;
                @StatsShown.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnStatsShown;
                @UseBandage.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnUseBandage;
                @UseBandage.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnUseBandage;
                @UseBandage.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnUseBandage;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveBinds.started += instance.OnMoveBinds;
                @MoveBinds.performed += instance.OnMoveBinds;
                @MoveBinds.canceled += instance.OnMoveBinds;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @MoveMira.started += instance.OnMoveMira;
                @MoveMira.performed += instance.OnMoveMira;
                @MoveMira.canceled += instance.OnMoveMira;
                @DisparoPrimario.started += instance.OnDisparoPrimario;
                @DisparoPrimario.performed += instance.OnDisparoPrimario;
                @DisparoPrimario.canceled += instance.OnDisparoPrimario;
                @DisparoSecundario.started += instance.OnDisparoSecundario;
                @DisparoSecundario.performed += instance.OnDisparoSecundario;
                @DisparoSecundario.canceled += instance.OnDisparoSecundario;
                @ApuntarMira.started += instance.OnApuntarMira;
                @ApuntarMira.performed += instance.OnApuntarMira;
                @ApuntarMira.canceled += instance.OnApuntarMira;
                @Accion.started += instance.OnAccion;
                @Accion.performed += instance.OnAccion;
                @Accion.canceled += instance.OnAccion;
                @RestarPartida.started += instance.OnRestarPartida;
                @RestarPartida.performed += instance.OnRestarPartida;
                @RestarPartida.canceled += instance.OnRestarPartida;
                @ShopOpen.started += instance.OnShopOpen;
                @ShopOpen.performed += instance.OnShopOpen;
                @ShopOpen.canceled += instance.OnShopOpen;
                @BuildOpen.started += instance.OnBuildOpen;
                @BuildOpen.performed += instance.OnBuildOpen;
                @BuildOpen.canceled += instance.OnBuildOpen;
                @Pausar.started += instance.OnPausar;
                @Pausar.performed += instance.OnPausar;
                @Pausar.canceled += instance.OnPausar;
                @Dialogo.started += instance.OnDialogo;
                @Dialogo.performed += instance.OnDialogo;
                @Dialogo.canceled += instance.OnDialogo;
                @StatsShown.started += instance.OnStatsShown;
                @StatsShown.performed += instance.OnStatsShown;
                @StatsShown.canceled += instance.OnStatsShown;
                @UseBandage.started += instance.OnUseBandage;
                @UseBandage.performed += instance.OnUseBandage;
                @UseBandage.canceled += instance.OnUseBandage;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Navigate;
    private readonly InputAction m_UI_Point;
    private readonly InputAction m_UI_Click;
    private readonly InputAction m_UI_Submit;
    private readonly InputAction m_UI_ScrollWheel;
    private readonly InputAction m_UI_ShopClose;
    private readonly InputAction m_UI_BuildClose;
    private readonly InputAction m_UI_Pausar;
    private readonly InputAction m_UI_CloseUI;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_UI_Navigate;
        public InputAction @Point => m_Wrapper.m_UI_Point;
        public InputAction @Click => m_Wrapper.m_UI_Click;
        public InputAction @Submit => m_Wrapper.m_UI_Submit;
        public InputAction @ScrollWheel => m_Wrapper.m_UI_ScrollWheel;
        public InputAction @ShopClose => m_Wrapper.m_UI_ShopClose;
        public InputAction @BuildClose => m_Wrapper.m_UI_BuildClose;
        public InputAction @Pausar => m_Wrapper.m_UI_Pausar;
        public InputAction @CloseUI => m_Wrapper.m_UI_CloseUI;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Point.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Click.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Submit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @ScrollWheel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @ShopClose.started -= m_Wrapper.m_UIActionsCallbackInterface.OnShopClose;
                @ShopClose.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnShopClose;
                @ShopClose.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnShopClose;
                @BuildClose.started -= m_Wrapper.m_UIActionsCallbackInterface.OnBuildClose;
                @BuildClose.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnBuildClose;
                @BuildClose.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnBuildClose;
                @Pausar.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPausar;
                @Pausar.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPausar;
                @Pausar.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPausar;
                @CloseUI.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCloseUI;
                @CloseUI.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCloseUI;
                @CloseUI.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCloseUI;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @ShopClose.started += instance.OnShopClose;
                @ShopClose.performed += instance.OnShopClose;
                @ShopClose.canceled += instance.OnShopClose;
                @BuildClose.started += instance.OnBuildClose;
                @BuildClose.performed += instance.OnBuildClose;
                @BuildClose.canceled += instance.OnBuildClose;
                @Pausar.started += instance.OnPausar;
                @Pausar.performed += instance.OnPausar;
                @Pausar.canceled += instance.OnPausar;
                @CloseUI.started += instance.OnCloseUI;
                @CloseUI.performed += instance.OnCloseUI;
                @CloseUI.canceled += instance.OnCloseUI;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // Construction
    private readonly InputActionMap m_Construction;
    private IConstructionActions m_ConstructionActionsCallbackInterface;
    private readonly InputAction m_Construction_Movimiento;
    private readonly InputAction m_Construction_BuildCancel;
    private readonly InputAction m_Construction_PlaceBuild;
    private readonly InputAction m_Construction_Pausar;
    private readonly InputAction m_Construction_CloseUI;
    public struct ConstructionActions
    {
        private @PlayerControls m_Wrapper;
        public ConstructionActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movimiento => m_Wrapper.m_Construction_Movimiento;
        public InputAction @BuildCancel => m_Wrapper.m_Construction_BuildCancel;
        public InputAction @PlaceBuild => m_Wrapper.m_Construction_PlaceBuild;
        public InputAction @Pausar => m_Wrapper.m_Construction_Pausar;
        public InputAction @CloseUI => m_Wrapper.m_Construction_CloseUI;
        public InputActionMap Get() { return m_Wrapper.m_Construction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ConstructionActions set) { return set.Get(); }
        public void SetCallbacks(IConstructionActions instance)
        {
            if (m_Wrapper.m_ConstructionActionsCallbackInterface != null)
            {
                @Movimiento.started -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnMovimiento;
                @Movimiento.performed -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnMovimiento;
                @Movimiento.canceled -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnMovimiento;
                @BuildCancel.started -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnBuildCancel;
                @BuildCancel.performed -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnBuildCancel;
                @BuildCancel.canceled -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnBuildCancel;
                @PlaceBuild.started -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnPlaceBuild;
                @PlaceBuild.performed -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnPlaceBuild;
                @PlaceBuild.canceled -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnPlaceBuild;
                @Pausar.started -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnPausar;
                @Pausar.performed -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnPausar;
                @Pausar.canceled -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnPausar;
                @CloseUI.started -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnCloseUI;
                @CloseUI.performed -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnCloseUI;
                @CloseUI.canceled -= m_Wrapper.m_ConstructionActionsCallbackInterface.OnCloseUI;
            }
            m_Wrapper.m_ConstructionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movimiento.started += instance.OnMovimiento;
                @Movimiento.performed += instance.OnMovimiento;
                @Movimiento.canceled += instance.OnMovimiento;
                @BuildCancel.started += instance.OnBuildCancel;
                @BuildCancel.performed += instance.OnBuildCancel;
                @BuildCancel.canceled += instance.OnBuildCancel;
                @PlaceBuild.started += instance.OnPlaceBuild;
                @PlaceBuild.performed += instance.OnPlaceBuild;
                @PlaceBuild.canceled += instance.OnPlaceBuild;
                @Pausar.started += instance.OnPausar;
                @Pausar.performed += instance.OnPausar;
                @Pausar.canceled += instance.OnPausar;
                @CloseUI.started += instance.OnCloseUI;
                @CloseUI.performed += instance.OnCloseUI;
                @CloseUI.canceled += instance.OnCloseUI;
            }
        }
    }
    public ConstructionActions @Construction => new ConstructionActions(this);

    // NoControles
    private readonly InputActionMap m_NoControles;
    private INoControlesActions m_NoControlesActionsCallbackInterface;
    private readonly InputAction m_NoControles_Newaction;
    public struct NoControlesActions
    {
        private @PlayerControls m_Wrapper;
        public NoControlesActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_NoControles_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_NoControles; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NoControlesActions set) { return set.Get(); }
        public void SetCallbacks(INoControlesActions instance)
        {
            if (m_Wrapper.m_NoControlesActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_NoControlesActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_NoControlesActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_NoControlesActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_NoControlesActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public NoControlesActions @NoControles => new NoControlesActions(this);
    private int m_TecladoSchemeIndex = -1;
    public InputControlScheme TecladoScheme
    {
        get
        {
            if (m_TecladoSchemeIndex == -1) m_TecladoSchemeIndex = asset.FindControlSchemeIndex("Teclado");
            return asset.controlSchemes[m_TecladoSchemeIndex];
        }
    }
    private int m_ControlSchemeIndex = -1;
    public InputControlScheme ControlScheme
    {
        get
        {
            if (m_ControlSchemeIndex == -1) m_ControlSchemeIndex = asset.FindControlSchemeIndex("Control");
            return asset.controlSchemes[m_ControlSchemeIndex];
        }
    }
    public interface IPlayerActionsActions
    {
        void OnMoveBinds(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnMoveMira(InputAction.CallbackContext context);
        void OnDisparoPrimario(InputAction.CallbackContext context);
        void OnDisparoSecundario(InputAction.CallbackContext context);
        void OnApuntarMira(InputAction.CallbackContext context);
        void OnAccion(InputAction.CallbackContext context);
        void OnRestarPartida(InputAction.CallbackContext context);
        void OnShopOpen(InputAction.CallbackContext context);
        void OnBuildOpen(InputAction.CallbackContext context);
        void OnPausar(InputAction.CallbackContext context);
        void OnDialogo(InputAction.CallbackContext context);
        void OnStatsShown(InputAction.CallbackContext context);
        void OnUseBandage(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnShopClose(InputAction.CallbackContext context);
        void OnBuildClose(InputAction.CallbackContext context);
        void OnPausar(InputAction.CallbackContext context);
        void OnCloseUI(InputAction.CallbackContext context);
    }
    public interface IConstructionActions
    {
        void OnMovimiento(InputAction.CallbackContext context);
        void OnBuildCancel(InputAction.CallbackContext context);
        void OnPlaceBuild(InputAction.CallbackContext context);
        void OnPausar(InputAction.CallbackContext context);
        void OnCloseUI(InputAction.CallbackContext context);
    }
    public interface INoControlesActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
