; define el nombre de la solución tal cual está en el directorio
!define SOLUTION_NAME "Ball MiniGame"

; información sobre la aplicación
!define PRODUCT_NAME "Diamantes en el Cielo"
!define PRODUCT_VERSION "1.5"

; lo mismo con lo mismo
!define PRODUCT_PUBLISHER "ChipojoSoft"
!define PRODUCT_WEB_SITE "http://www.chipojosoft.com"




; De aquí pa abajo no toques nada


; Cadena que genera el producto instalado en el panel de control
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; pacotilla de la interfaz
!include "MUI.nsh"
!define MUI_ABORTWARNING
!define MUI_ICON  "${NSISDIR}\Contrib\Graphics\Icons\modern-install-full.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; las páginas del Wizard
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "C:\Users\lazaro\Documents\Licencias\Licencia_MIT_es.rtf"
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_LANGUAGE "Spanish"

; fichero de salida
Name "${PRODUCT_NAME} v${PRODUCT_VERSION}"
; OutFile "D:\Programas\${PRODUCT_NAME}_v${PRODUCT_VERSION}.exe"
OutFile "D:\Programas\Diamantes_en_el_Cielo_v${PRODUCT_VERSION}.exe"
InstallDir "$PROGRAMFILES\ChipojoSoft\${PRODUCT_NAME}"
ShowInstDetails show
ShowUnInstDetails show

; que ficheros 
Section "MainSection" SEC01

	; dónde?
	SetOutPath "$INSTDIR"
	SetOverwrite ifnewer

	; sobreescribe lo viejo
	SetOverwrite on



	; mete TODO el contenido de la carpeta (build\*) y recursivo (/r)
	File /r "build\*"
 
	; el ícono del escritorio
	CreateShortCut "$DESKTOP\${PRODUCT_NAME}.lnk" "$INSTDIR\${PRODUCT_NAME}.exe"

	; el menú de inicio
	CreateDirectory "$SMPROGRAMS\${PRODUCT_NAME}"
	CreateShortCut "$SMPROGRAMS\${PRODUCT_NAME}\${PRODUCT_NAME}.lnk" "$INSTDIR\${PRODUCT_NAME}.exe"
	CreateShortcut "$SMPROGRAMS\${PRODUCT_NAME}\uninstall.lnk" "$INSTDIR\uninst.exe"

SectionEnd




; eliminar del registro los siguientes valores de la instalación
Section -Post

  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
  
SectionEnd

Function .onInit
	 System::Call 'kernel32::CreateMutex(p 0, i 0, t "myMutex") p .r1 ?e'
	 Pop $R0
 
	 StrCmp $R0 0 +3
	   MessageBox MB_OK|MB_ICONEXCLAMATION "Solo una instancia a la vez."
	   Abort
FunctionEnd


; el desinstalador
Section Uninstall

	; borra todo el contenido del directorio instalado
	Delete "$INSTDIR\*.*"

	; borra el propio directorio
	RMDir "$INSTDIR"

	; borra el acceso del escritorio
	Delete "$DESKTOP\${PRODUCT_NAME}.lnk"

	; borra el directorio del menu de inicio
	Delete "$SMPROGRAMS\${PRODUCT_NAME}\*.*"
	RMDir "$SMPROGRAMS\${PRODUCT_NAME}"

	; borra la calve de registro que muestra el desinstalador en el panel de control
	DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"

	; no cierres
	SetAutoClose false

SectionEnd