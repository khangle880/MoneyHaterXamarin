package crc64950784d87e601e91;


public class AccountService
	extends com.google.firebase.auth.PhoneAuthProvider.OnVerificationStateChangedCallbacks
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onVerificationCompleted:(Lcom/google/firebase/auth/PhoneAuthCredential;)V:GetOnVerificationCompleted_Lcom_google_firebase_auth_PhoneAuthCredential_Handler\n" +
			"n_onVerificationFailed:(Lcom/google/firebase/FirebaseException;)V:GetOnVerificationFailed_Lcom_google_firebase_FirebaseException_Handler\n" +
			"n_onCodeSent:(Ljava/lang/String;Lcom/google/firebase/auth/PhoneAuthProvider$ForceResendingToken;)V:GetOnCodeSent_Ljava_lang_String_Lcom_google_firebase_auth_PhoneAuthProvider_ForceResendingToken_Handler\n" +
			"";
		mono.android.Runtime.register ("MoneyHater.Droid.Services.AccountService, MoneyHater.Android", AccountService.class, __md_methods);
	}


	public AccountService ()
	{
		super ();
		if (getClass () == AccountService.class)
			mono.android.TypeManager.Activate ("MoneyHater.Droid.Services.AccountService, MoneyHater.Android", "", this, new java.lang.Object[] {  });
	}


	public void onVerificationCompleted (com.google.firebase.auth.PhoneAuthCredential p0)
	{
		n_onVerificationCompleted (p0);
	}

	private native void n_onVerificationCompleted (com.google.firebase.auth.PhoneAuthCredential p0);


	public void onVerificationFailed (com.google.firebase.FirebaseException p0)
	{
		n_onVerificationFailed (p0);
	}

	private native void n_onVerificationFailed (com.google.firebase.FirebaseException p0);


	public void onCodeSent (java.lang.String p0, com.google.firebase.auth.PhoneAuthProvider.ForceResendingToken p1)
	{
		n_onCodeSent (p0, p1);
	}

	private native void n_onCodeSent (java.lang.String p0, com.google.firebase.auth.PhoneAuthProvider.ForceResendingToken p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
