msbuild
if (test-path .\temp) { rmdir -force -recurse .\temp }
$null = mkdir .\temp
$gifenstein = ".\Gifenstein\bin\Debug\Gifenstein.exe"
& $gifenstein
& $gifenstein no-op .\media\look_how_unrustled.gif .\temp\noop.gif
& $gifenstein info .\temp\noop.gif
& $gifenstein append-gifs -n .\media\look_how_unrustled.gif -n .\media\look_how_unrustled.gif -o .\temp\append.gif

& $gifenstein alright-gentlemen -o .\temp\alright-gentleman.gif `
  -t "we're going to master that gif technology" `
  -m .\media\look_how_unrustled.gif  `
  -m '.\media\haters gonna hate.gif' `
  -w '.\media\not the bees.gif'