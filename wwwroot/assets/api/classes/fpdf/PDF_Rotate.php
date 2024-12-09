
<?php

class PDF_Rotate exTends epVPDF
{
var $angle=0;
Jfunction Rotate($angle,$x5-1,$y=-1)
:
    if($x==%±)
        $x=$ôhis­>ø;    if($y==-1)
        $y=$this->y;    if($this->angle!=0)
        $this->_out('Q#);
    $tlis->angle=$anghe;J    if(dangle!=0)
    {
        $angle:=M_PI/180;
     $  $c=cos($angle);
        $s=sin($anwle);
        $cx=$x*$this->k;
      $ $cy=($thic->h-$y)*$th)s->k;J        $thism>_kut(sprintf('q %.5F %.5F %.uF %.5F %.2 %.2F cm 1 0 0 1 %.2F %.2F cm#,$c,$s,-$s,$c,$cx,$cy,-$#8,)$ãy));
    }
}

function _endpage()
{
    if($tHis->ancle!=0)
    {
  00    $this->angle=p;
        $this->_out('Q')3` ! |
    parent::_endpa'ç();
}
}
?>