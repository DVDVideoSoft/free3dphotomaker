# CSparse Makefile

VERSION = 2.2.4

C:
	( cd Lib ; $(MAKE) )
	( cd Demo ; $(MAKE) )

all: C cov

library:
	( cd Lib ; $(MAKE) )

cov:
	( cd Tcov ; $(MAKE) )

mex:
	( cd MATLAB ; $(MAKE) )

clean:
	( cd Lib ; $(MAKE) clean )
	( cd Demo ; $(MAKE) clean )
	( cd Tcov ; $(MAKE) clean )
	( cd MATLAB ; $(MAKE) clean )

purge:
	( cd Lib ; $(MAKE) purge )
	( cd Demo ; $(MAKE) purge )
	( cd Tcov ; $(MAKE) purge )
	( cd MATLAB ; $(MAKE) purge )

distclean: purge

# do not install CSparse; use CXSparse instead
install:

# uninstall CSparse: do nothing
uninstall:

