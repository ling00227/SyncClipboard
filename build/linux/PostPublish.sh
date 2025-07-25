#!/bin/bash
# This is a dummy bash script used for demonstration and test. It outputs a few variables
# and creates a dummy file in the application directory which will be detected by the program.

echo
echo ===========================
echo POST_PUBLISH BASH SCRIPT
echo ===========================
echo

# Some useful macros  environment variables
echo BUILD_ARCH ${BUILD_ARCH}
echo BUILD_TARGET ${BUILD_TARGET}
echo BUILD_SHARE ${BUILD_SHARE}
echo BUILD_APP_BIN ${BUILD_APP_BIN}
echo

echo Do work...
set -x #echo on
echo Copying files
# build on Windows first, put outputs in [../linux/] foleder
build_bin_dir=$(readlink -f './build_bin')
echo build_bin_dir full path : $bin_source_dir

cp -r ./build_bin/* ${BUILD_APP_BIN}/

cat > "${BUILD_APP_BIN}/xyz.jericx.desktop.syncclipboard.desktop" <<EOF
[Desktop Entry]
Type=Application
Name=${APP_FRIENDLY_NAME}
Icon=${APP_ID}
Comment=${APP_SHORT_SUMMARY}
Exec=env LANG=en_US.UTF-8 ${INSTALL_EXEC}
TryExec=${INSTALL_EXEC}
NoDisplay=${DESKTOP_NODISPLAY}
X-AppImage-Integrate=${DESKTOP_INTEGRATE}
Terminal=${DESKTOP_TERMINAL}
Categories=${PRIME_CATEGORY}
StartupWMClass=${APP_BASE_NAME}
MimeType=
Keywords=
EOF

set +x #echo off

echo
echo ===========================
echo POST_PUBLISH END
echo ===========================
echo