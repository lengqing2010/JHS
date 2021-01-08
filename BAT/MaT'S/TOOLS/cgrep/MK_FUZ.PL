#!jperl
#
#usage: jperl mk_fuz.pl fuz.org > cgrep.fuz
#

while (<>) {
	if (/^#/) {
		print;
		next;
	}
	chop;
	$org = $_;
	while ($org) {
		$org =~ /^(.)(.*)$/;
		print "$1:[$_]\n";
		$org = $2;
	}
}
